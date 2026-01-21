
# 3. Entorno de Container Apps
resource "azurerm_container_app_environment" "env" {
  name                = "msbankenvironment"
  location            = var.location
  resource_group_name = var.resource_group_name
}

# 4. Container App (Usando imagen de prueba para evitar errores)
resource "azurerm_container_app" "app" {
  name                         = "cqrsappjllv"
  container_app_environment_id = azurerm_container_app_environment.env.id
  resource_group_name          = var.resource_group_name
  revision_mode                = "Single"

  identity { type = "SystemAssigned" }

  ingress {
    external_enabled           = true   # Esto genera la URL pública
    target_port                = 8080   # El puerto que configuraste en tu Dockerfile
    allow_insecure_connections = true   # Agrégale esta línea
    transport                  = "auto" # Agrégale esta línea
    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }


  template {
    container {
      name   = "app-cqrs"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Production"
      }
      env {
        name  = "KEYVAULTURL"
        value = var.key_value_url
      }
    }
  }

  # IMPORTANTE: Ignorar cambios en la imagen para que Terraform 
  # no intente borrar lo que el Pipeline despliegue después.
  lifecycle {
    ignore_changes = [
      template[0].container[0].image,
    ]
  }
  depends_on = [azurerm_container_app_environment.env]
}

# 5. Permiso: La App puede leer el Key Vault
resource "azurerm_role_assignment" "app_to_kv" {
  scope                = var.key_value_id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_container_app.app.identity[0].principal_id
  depends_on           = [azurerm_container_app.app]

}
