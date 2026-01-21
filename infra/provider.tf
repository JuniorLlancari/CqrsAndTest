provider "azurerm" {
  subscription_id = var.subscription_id

  # Solo usa credenciales si están disponibles (local)
  client_id     = var.client_id != "" ? var.client_id : null
  client_secret = var.client_secret != "" ? var.client_secret : null
  tenant_id     = var.tenant_id != "" ? var.tenant_id : null

  # OIDC se activa automáticamente en CI/CD si hay ARM_USE_OIDC=true
  use_oidc = var.use_oidc

  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
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
  }

  required_version = ">= 1.1.0"
}
