provider "azurerm" {
  subscription_id = var.subscription_id
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
  use_oidc = true
}

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "rg-terraform-state"
    storage_account_name = "stcorebankstate01"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"


    # Muy importante para que el comando 'init' en el pipeline no falle
    use_oidc = true
  }

  required_version = ">= 1.1.0"
}
