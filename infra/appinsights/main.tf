resource "azurerm_application_insights" "insights" {
  name                = var.nombre_appinsights
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = "web"
  workspace_id        = azurerm_log_analytics_workspace.law.id

  depends_on = [azurerm_log_analytics_workspace.law]
}

resource "azurerm_log_analytics_workspace" "law" {
  name                = "loganaliticsnombrews"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "PerGB2018"
}
