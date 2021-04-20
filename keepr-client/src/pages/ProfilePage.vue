<template>
  <div class="profile-page container-fluid">
    <div v-if="state.profile.name">
      <div class="d-flex justify-content-center mt-5">
        <div class="col-10">
          <div class="row">
            <div class="col d-flex">
              <img class="rounded" :src="state.profile.picture" alt="" />
              <div class="ml-4">
                <h2 class="text-capitalize">
                  {{ name(state.profile.name) }}
                </h2>
                <h5>Vaults: {{ state.vaults.length }}</h5>
                <h5>Keeps: {{ state.keeps.length }}</h5>
              </div>
            </div>
          </div>

          <div id="profileVaults">
            <div class="row">
              <div class="col-12 d-flex mt-5">
                <h4>Vaults</h4>
                <div v-if="state.profile.email == state.user.email">
                  <button
                    class="btn"
                    type="button"
                    id="hover-button"
                    data-toggle="modal"
                    data-target="#addVault"
                    aria-hidden="true"
                  >
                    <i class="fa fa-plus-circle" aria-hidden="true"></i>
                  </button>
                  <AddVaultModal />
                </div>
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

          <div id="profileKeeps">
            <div class="row">
              <div class="col-12 d-flex mt-5">
                <h4>Keeps</h4>
                <div v-if="state.profile.email == state.user.email">
                  <button
                    class="btn"
                    type="button"
                    id="hover-button"
                    data-toggle="modal"
                    data-target="#addKeep"
                    aria-hidden="true"
                  >
                    <i class="fa fa-plus-circle" aria-hidden="true"></i>
                  </button>
                  <AddKeepModal />
                </div>
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
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { onBeforeRouteLeave, useRoute } from 'vue-router'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    })
    onMounted(async () => {
      await profilesService.getProfile(route.params.id)
      await profilesService.getProfileVaults(route.params.id)
      await profilesService.getProfileKeeps(route.params.id)
    })
    onBeforeRouteLeave((to, from, next) => {
      AppState.profile = {}
      next()
    })
    return {
      state,
      name(n) {
        return n.includes('@')
          ? n.split('@')[0]
          : n
      }
    }
  },
  components: {}
}
</script>
<style scoped>
</style>
