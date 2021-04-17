<template>
  <div class="Vault card p-3 border-rounded">
    <div class="card-body">
      <router-link :to="{ name: 'VaultDetailsPage', params: { id: vault.id } }">
        <h3 class="card-title text-dark">
          {{ vault.name }}
        </h3>
      </router-link>
    </div>
  </div>
</template>

<script>
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Vault',
  props: {
    vault: { type: Object, required: true }
  },
  setup(props) {
    return {
      async getVault() {
        await vaultsService.getVault(props.vault.id)
      }
    }
  },
  components: {}
}
</script>
