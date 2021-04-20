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
          <div class="modal-body">
            <div class="row">
              <div class="col-6 left-side d-flex">
                <img class="img-fluid keep-img" :src="keep.img" />
              </div>
              <div class="col-6 my-2 container-fluid">
                <div class="row">
                  <div class="col move">
                    <button
                      type="button"
                      class="close"
                      data-dismiss="modal"
                      aria-label="Close"
                    >
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                </div>
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
                <div class="row justify-content-center mt-2 description">
                  <div class="col-10">
                    <h6>{{ keep.description }}</h6>
                  </div>
                </div>
                <div class="row justify-content-between">
                  <div class="col-3">
                    <div class="btn-group dropup">
                      <button
                        type="button"
                        class="btn btn-primary dropdown-toggle"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false"
                      >
                        Save
                      </button>
                      <div class="dropdown-menu scroll">
                        <p class="dropdown-item" @click="addToNewVault">
                          Create A Vault
                        </p>
                        <p
                          class="dropdown-item"
                          v-for="vault in state.vaults"
                          :key="vault.id"
                          @click="addToVault(vault)"
                        >
                          {{ vault.name }}
                        </p>
                      </div>
                    </div>
                  </div>
                  <div
                    class="col-1"
                    v-if="keep.creator.email == state.user.email"
                  >
                    <button
                      type="button"
                      class="btn btn-danger mt-1"
                      @click="deleteKeep"
                    >
                      <i class="fa fa-trash" aria-hidden="true"></i>
                    </button>
                  </div>
                  <div class="col-6">
                    <router-link
                      :to="{
                        name: 'ProfilePage',
                        params: { id: keep.creatorId },
                      }"
                      class="d-flex justify-content-center text-dark"
                      @click="closeModal"
                    >
                      <img
                        class="profile-picture"
                        :src="keep.creator.picture"
                        alt=""
                      />
                      <p class="mt-2 ml-2">
                        {{
                          keep.creator.name.includes("@")
                            ? keep.creator.name.split("@")[0]
                            : keep.creator.name
                        }}
                      </p>
                    </router-link>
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
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import Swal from 'sweetalert2'
import { useRouter } from 'vue-router'

export default {
  name: 'ViewKeepModal',
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.userVaults),
      activeKeep: computed(() => AppState.activeKeep),
      newVK: {}
    })
    return {
      state,
      closeModal() {
        $('#view-keep-' + props.keep.id).modal('hide')
        keepsService.clearAK()
      },
      async addToVault(vault) {
        state.newVK.vaultID = vault.id
        state.newVK.keepId = props.keep.id
        await vaultKeepsService.create(state.newVK)
        Swal.fire({
          title: 'Keep added to ' + vault.name + '!',
          icon: 'success',
          showCloseButton: true,
          showCancelButton: true,
          confirmButtonText: 'Take Me To ' + vault.name + '',
          cancelButtonText: 'Stay on Keep'
        }).then((result) => {
          if (result.isConfirmed) {
            $('#view-keep-' + props.keep.id).modal('hide')
            router.push({ name: 'VaultDetailsPage', params: { id: vault.id } })
          }
        })
        state.newVK = {}
      },
      deleteKeep() {
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then(async (result) => {
          if (result.isConfirmed) {
            await keepsService.deleteKeep(props.keep)
            $('#view-keep-' + props.keep.id).modal('hide')
            Swal.fire(
              'Deleted!',
              'Your keep has been deleted.',
              'success'
            )
          }
        })
      },
      addToNewVault() {
        $('#view-keep-' + props.keep.id).modal('hide')
        $('#addToNewVault').modal('show')
      }
    }
  },
  components: {}
}
</script>

<style scoped>
.left-side {
  height: 100%;
}

.keep-img {
  align-content: center;
}

.scroll {
  max-height: 200px;
  overflow-y: auto;
}

@media (min-width: 991.5px) {
  .description {
    min-height: 60%;
  }
}

@media (max-width: 991px) {
  .description {
    min-height: 30%;
  }
}

.profile-picture {
  max-height: 45px;
}

.move {
  padding-right: 10px !important;
  margin-top: -15px !important;
  margin-bottom: 10px;
}
</style>
