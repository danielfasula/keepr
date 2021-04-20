<template>
  <div class="account-page container-fluid">
    <div
      v-if="state.account.name"
      class="row d-flex justify-content-center mt-5"
    >
      <div class="col-10">
        <div class="row">
          <div class="col d-flex">
            <img class="rounded" :src="state.account.picture" alt="" />
            <div class="ml-4">
              <h2>
                {{ name(state.account.name) }}
                <!-- state.account.name.includes("@")
                    ? state.account.name.split("@")[0].charAt(0).toUpperCase() +
                      state.account.name.split("@")[0].slice(1)
                    : state.account.name -->
              </h2>
              <h5>Vaults: {{ state.vaults.length }}</h5>
              <h5>Keeps: {{ state.keeps.length }}</h5>
            </div>
          </div>
        </div>
        <div id="userVaults">
          <div class="row">
            <div class="col-12 d-flex mt-5">
              <h4>Vaults</h4>
              <button
                class="btn"
                type="button"
                data-toggle="modal"
                data-target="#addVault"
                aria-hidden="true"
              >
                <i class="fa fa-plus-circle" aria-hidden="true"></i>
              </button>
              <AddVaultModal />
            </div>
          </div>
          <div class="row stop">
            <div class="masonry-columns mt-2">
              <Vault
                v-for="vault in state.vaults"
                :key="vault.id"
                :vault="vault"
              />
            </div>
          </div>
        </div>

        <div id="userKeeps">
          <div class="row">
            <div class="col-12 d-flex mt-5">
              <h4>Keeps</h4>
              <button
                class="btn"
                type="button"
                data-toggle="modal"
                data-target="#addKeep"
                aria-hidden="true"
              >
                <i class="fa fa-plus-circle" aria-hidden="true"></i>
              </button>
              <AddKeepModal />
            </div>
          </div>
          <div class="row stop">
            <div class="masonry-columns mt-2">
              <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.userVaults),
      keeps: computed(() => AppState.userKeeps)
    })
    onMounted(async () => {
      await accountService.getVaults()
      await accountService.getKeeps()
    })
    return {
      state,
      name(n) {
        return n.includes('@')
          ? n.split('@')[0].charAt(0).toUpperCase() +
          n.split('@')[0].slice(1)
          : n
      }
    }
  }
}
</script>

<style scoped >
img {
  max-width: 100px;
}
.btn {
  margin-top: -5px;
}
</style>
