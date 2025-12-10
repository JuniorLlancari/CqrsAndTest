variable "location" {
  type        = string
  description = "Azure region where resources will be deployed"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}



variable "db_name_sqlserver" {
  type        = string
  description = "Database name"
}

variable "server_name_sqlserver" {
  type        = string
  description = "SQL Server logical name"
}

variable "admin_user_sqlserver" {
  type        = string
  description = "Admin username for SQL Server"
}

variable "admin_password_sqlserver" {
  type        = string
  description = "Admin password for SQL Server"
  sensitive   = true
}
