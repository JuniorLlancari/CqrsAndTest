
data "azurerm_client_config" "current" {}


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


resource "azurerm_role_assignment" "rolportal" {
  scope                = azurerm_key_vault.key_valult.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = var.portal_id
}
resource "azurerm_role_assignment" "rolterraform" {
  scope                = azurerm_key_vault.key_valult.id
  role_definition_name = "Key Vault Administrator"
  principal_id         = data.azurerm_client_config.current.object_id

}


resource "azurerm_key_vault_secret" "cndatabase" {
  name         = "CNSTRINGDBSQLSERVER"
  value        = var.CN_STRING_DB_SQLSERVER
  key_vault_id = azurerm_key_vault.key_valult.id
  depends_on   = [azurerm_role_assignment.rolterraform]

}



resource "azurerm_key_vault_secret" "cninsights" {
  name         = "CNSTRINGAPPINSIGHTS"
  value        = var.CN_STRING_APP_INSIGHTS
  key_vault_id = azurerm_key_vault.key_valult.id
  depends_on   = [azurerm_role_assignment.rolterraform]
}


