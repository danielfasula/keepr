<template>
  <div class="box border p-0">
    <router-link :to="{ name: 'VaultDetailsPage', params: { id: vault.id } }">
      <h3 class="text-dark">
        {{ vault.name }}
      </h3>
    </router-link>
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
.box {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background: white;
}
</style>
