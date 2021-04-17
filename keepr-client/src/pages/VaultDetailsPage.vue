<template>
  <div class="vault-details-page container-fluid">
    <div class="row d-flex justify-content-center" v-if="state.vault.creator">
      <i class="fas fa-spinner fa-spin" v-if="state.loading"></i>
      <div
        v-else
        class="col-10 d-flex justify-content-around align-items-center mt-5"
      >
        <h1>
          {{ state.vault.name }}
        </h1>
        <div v-if="state.vault.creator.email == state.user.email">
          <i
            type="button"
            class="fa fa-trash text-danger"
            aria-hidden="true"
            @click="deleteVault"
          ></i>
        </div>
      </div>
    </div>

    <div class="row">
      <div class="col-12 d-flex mt-5">
        <h4>Keeps: {{ state.keeps.length }}</h4>
      </div>
    </div>
    <div class="row">
      <div class="card-columns mt-5">
        <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { onBeforeRouteLeave, useRoute, useRouter } from 'vue-router'
import Swal from 'sweetalert2'
export default {
  name: 'VaultDetailsPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      loading: true,
      user: computed(() => AppState.user),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps)
    })
    onMounted(async () => {
      await vaultsService.getVault(route.params.id)
      await vaultsService.getVaultKeeps(route.params.id)
      state.loading = false
    })
    onBeforeRouteLeave((to, from, next) => {
      AppState.activeVault = {}
      next()
    })
    return {
      state,
      deleteVault() {
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            vaultsService.deleteVault(state.vault.id)
            router.back()
            Swal.fire(
              'Deleted!',
              'Your vault has been deleted.',
              'success'
            )
          }
        })
      }
    }
  },
  components: {}
}
</script>
