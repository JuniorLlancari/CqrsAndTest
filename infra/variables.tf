variable "subscription_id" {
  type    = string
  default = ""

}
variable "portal_id" {
  type    = string
  default = ""
}

variable "resource_group_name" {
  type = string
}
variable "location" {
  type = string
}




variable "server_name_sqlserver" {
  type = string
}

variable "admin_user_sqlserver" {
  type = string
}

variable "admin_password_sqlserver" {
  type      = string
  sensitive = true
}

variable "db_name_sqlserver" {
  type = string
}



