variable "location" {
  type        = string
  description = "Azure region where resources will be deployed"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}


variable "CN_STRING_DB_SQLSERVER" {
  type = string
}

variable "CN_STRING_APP_INSIGHTS" {
  type = string
}
variable "portal_id" {
  type = string
}

variable "serviceprincipal_id" {
  type = string
}
