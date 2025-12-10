variable "location" {
  type        = string
  description = "Azure region where resources will be deployed"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "nombre_appinsights" {
  type = string
}
