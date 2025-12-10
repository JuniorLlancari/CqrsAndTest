#SQL SERVER OUTPUTS
output "sql_server_name" {
  value = azurerm_mssql_server.sql_server.name
}
output "sql_server_fqdomain_name" {
  value = azurerm_mssql_server.sql_server.fully_qualified_domain_name
}
output "sql_server_administrator_login" {
  value = azurerm_mssql_server.sql_server.administrator_login
}
output "sql_server_administrator_login_password" {
  value = azurerm_mssql_server.sql_server.administrator_login_password
}
output "db_name_sqlserver" {
  value = azurerm_mssql_database.database.name
}



