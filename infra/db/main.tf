# Servidor lógico de Azure SQL
resource "azurerm_mssql_server" "sql_server" {
  name                         = var.server_name_sqlserver
  resource_group_name          = var.resource_group_name
  location                     = var.location
  version                      = "12.0"
  administrator_login          = var.admin_user_sqlserver
  administrator_login_password = var.admin_password_sqlserver
}

resource "azurerm_mssql_firewall_rule" "allow_azure" {
  name             = "AllowAzureServices"
  server_id        = azurerm_mssql_server.sql_server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}

resource "azurerm_mssql_database" "database" {
  name                 = var.db_name_sqlserver
  server_id            = azurerm_mssql_server.sql_server.id
  sku_name             = "Basic"
  max_size_gb          = 2
  storage_account_type = "Local"
  zone_redundant       = false
}


