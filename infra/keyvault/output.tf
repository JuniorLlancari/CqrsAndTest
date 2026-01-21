output "key_vault_id" {
  value = azurerm_key_vault.key_valult.id
}

output "key_vault_url" {
  value = azurerm_key_vault.key_valult.vault_uri
}
