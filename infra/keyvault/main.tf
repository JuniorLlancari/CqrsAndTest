
data "azurerm_client_config" "current" {}




resource "azurerm_key_vault" "key_valult" {
  name                = "servicekeyvalult"
  location            = var.location
  resource_group_name = var.resource_group_name
  tenant_id           = data.azurerm_client_config.current.tenant_id

  sku_name = "standard"
  # soft_delete_retention_days = 7

  access_policy {
    tenant_id          = data.azurerm_client_config.current.tenant_id
    object_id          = data.azurerm_client_config.current.object_id
    key_permissions    = ["Get", "List", "Create", "Update", "Delete", "Recover"]
    secret_permissions = ["Get", "List", "Set", "Delete", "Recover"]
  }



}


resource "azurerm_role_assignment" "rolkv" {
  scope                = azurerm_key_vault.key_valult.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = data.azurerm_client_config.current.object_id

}


resource "azurerm_key_vault_secret" "cndatabase" {
  name         = "CNSTRINGDBSQLSERVER"
  value        = var.CN_STRING_DB_SQLSERVER
  key_vault_id = azurerm_key_vault.key_valult.id

}



resource "azurerm_key_vault_secret" "cninsights" {
  name         = "CNSTRINGAPPINSIGHTS"
  value        = var.CN_STRING_APP_INSIGHTS
  key_vault_id = azurerm_key_vault.key_valult.id
}


