<template>
  <div class="view-keep-modal container-fluid" v-if="keep.creator">
    <div
      class="modal fade"
      :id="'view-keep-' + keep.id"
      tabindex="-1"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-6">
                <img class="keep-img img-fluid" :src="keep.img" />
              </div>
              <div class="col-6 my-2 container-fluid">
                <div class="row">
                  <div class="col d-flex justify-content-center">
                    <div class="mx-2">
                      <i class="far fa-eye"></i> {{ keep.views }}
                    </div>
                    <div class="mx-2">
                      <i class="fas fa-share-alt"></i> {{ keep.shares }}
                    </div>
                    <div class="mx-2">
                      <i class="fab fa-kickstarter-k"></i> {{ keep.keeps }}
                    </div>
                  </div>
                </div>
                <div class="row my-3">
                  <div class="col-12 text-center">
                    <h4>{{ keep.name }}</h4>
                  </div>
                </div>
                <div class="row justify-content-center mt-2">
                  <div class="col-10">
                    <h6>{{ keep.description }}</h6>
                  </div>
                </div>
                <div class="row justify-content-between">
                  <div class="col-4">
                    <div class="btn-group dropup">
                      <button
                        type="button"
                        class="btn btn-primary dropdown-toggle"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false"
                      >
                        Add To Vault
                      </button>
                      <div class="dropdown-menu">
                        <a class="dropdown-item" href="#"> Create A Vault </a>
                        <a
                          class="dropdown-item"
                          href="#"
                          v-for="vault in state.vaults"
                          :key="vault.id"
                          @click="addToVault(vault.id)"
                        >
                          {{ vault.name }}
                        </a>
                      </div>
                    </div>
                  </div>
                  <div class="col-2">
                    <button
                      type="button"
                      class="btn btn-danger"
                      @click="deleteKeep"
                    >
                      <i class="fa fa-trash" aria-hidden="true"></i>
                    </button>
                  </div>
                  <div class="col-4 d-flex">
                    <img
                      class="profile-picture"
                      :src="keep.creator.picture"
                      alt=""
                    />
                    <p>{{ keep.creator.name }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  name: 'ViewKeepModal',
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.userVaults),
      newVK: {}
    })
    return {
      state,
      async addToVault(vaultId) {
        state.newVK.vaultID = vaultId
        state.newVK.keepId = props.keep.id
        await vaultKeepsService.create(state.newVK)
        state.newVK = {}
      }
    }
  },
  components: {}
}
</script>

<style scoped>
.profile-picture {
  max-height: 50px;
}
</style>
