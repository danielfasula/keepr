<template>
  <div class="card p-0 my-2">
    <div class="card-body text-center">
      <router-link :to="{ name: 'VaultDetailsPage', params: { id: vault.id } }">
        <h3 class="text-dark">
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

<style scoped>
.card {
  border-radius: 15px;
  border-color: #55efc4;
  border-width: 5px;
}
</style>
