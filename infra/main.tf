resource "azurerm_resource_group" "rg" {
  location = var.location
  name     = var.resource_group_name
}


module "db" {
  source                   = "./db"
  location                 = var.location
  resource_group_name      = var.resource_group_name
  db_name_sqlserver        = var.db_name_sqlserver
  admin_password_sqlserver = var.admin_password_sqlserver
  admin_user_sqlserver     = var.admin_user_sqlserver
  server_name_sqlserver    = var.server_name_sqlserver
  depends_on               = [azurerm_resource_group.rg]
}

module "appinsights" {
  source              = "./appinsights"
  location            = var.location
  resource_group_name = var.resource_group_name
  nombre_appinsights  = "nameappinsights"
  depends_on          = [azurerm_resource_group.rg]
}


locals {
  sqlserver_connection = "Server=tcp:${module.db.sql_server_fqdomain_name},1433;Initial Catalog=${module.db.db_name_sqlserver};Persist Security Info=False;User ID=${module.db.sql_server_administrator_login};Password=${module.db.sql_server_administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}




module "keyvalut" {
  source                 = "./keyvault"
  location               = var.location
  resource_group_name    = var.resource_group_name
  CN_STRING_DB_SQLSERVER = local.sqlserver_connection
  CN_STRING_APP_INSIGHTS = module.appinsights.appinsights_connection_string
  portal_id              = var.portal_id
  depends_on             = [azurerm_resource_group.rg]
}
