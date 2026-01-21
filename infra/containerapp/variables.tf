variable "location" {
  type        = string
  description = "Azure region where resources will be deployed"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "key_value_url" {
  type        = string
  description = "Key Vault URL"
}
variable "key_value_id" {
  type        = string
  description = "Key Vault ID"
}
