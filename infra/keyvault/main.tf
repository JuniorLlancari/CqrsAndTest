
data "azurerm_client_config" "current" {}

# Agrega un sleep para que Azure propague el Key Vault

resource "random_id" "kvname" {
  byte_length = 3
}

resource "azurerm_key_vault" "key_valult" {
  name                      = "svckeyvalult${random_id.kvname.hex}"
  location                  = var.location
  resource_group_name       = var.resource_group_name
  tenant_id                 = data.azurerm_client_config.current.tenant_id
  sku_name                  = "standard"
  enable_rbac_authorization = true
}

resource "time_sleep" "wait_for_kv" {
  depends_on      = [azurerm_key_vault.key_valult]
  create_duration = "30s"
}
resource "azurerm_role_assignment" "rolportal" {
  scope                = azurerm_key_vault.key_valult.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = var.portal_id
  principal_type       = "User" # 👈 AGREGA ESTA LÍNEA

  # skip_service_principal_aad_check = true

  depends_on = [time_sleep.wait_for_kv]

}
resource "azurerm_role_assignment" "rolterraform" {
  scope                = azurerm_key_vault.key_valult.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = var.serviceprincipal_id #data.azurerm_client_config.current.object_id
  # skip_service_principal_aad_check = true
  # principal_type                   = "ServicePrincipal" # 👈 AGREGA ESTA LÍNEA
  depends_on = [time_sleep.wait_for_kv]
}


resource "azurerm_key_vault_secret" "cndatabase" {
  name         = "CNSTRINGDBSQLSERVER"
  value        = var.CN_STRING_DB_SQLSERVER
  key_vault_id = azurerm_key_vault.key_valult.id
  depends_on   = [azurerm_role_assignment.rolterraform, time_sleep.wait_for_kv]

}



resource "azurerm_key_vault_secret" "cninsights" {
  name         = "CNSTRINGAPPINSIGHTS"
  value        = var.CN_STRING_APP_INSIGHTS
  key_vault_id = azurerm_key_vault.key_valult.id
  depends_on   = [azurerm_role_assignment.rolterraform, time_sleep.wait_for_kv]
}


