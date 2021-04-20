<template>
  <div class="add-to-new-vault-modal container-fluid">
    <div
      class="modal fade"
      id="addToNewVault"
      tabindex="-1"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">New Vault</h5>
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
            <form @submit.prevent="addKeepToNewVault">
              <div class="form-group">
                <label for="title"> Title </label>
                <input
                  type="text"
                  class="form-control"
                  name="title"
                  id="title"
                  aria-describedby="emailHelp"
                  placeholder="Title..."
                  v-model="state.newVault.name"
                />
              </div>
              <div class="form-group">
                <label for="description"> Description </label>
                <input
                  type="text"
                  class="form-control"
                  name="description"
                  id="description"
                  placeholder="Description..."
                  v-model="state.newVault.description"
                />
              </div>
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="defaultCheck1"
                  v-model="state.newVault.isPrivate"
                />
                <label class="form-check-label" for="defaultCheck1">
                  Private?
                  <small id="vaultHelp" class="form-text text-muted">
                    Private vaults can only be seen by you.
                  </small>
                </label>
              </div>
              <div class="row">
                <div class="col d-flex justify-content-end">
                  <button type="submit" class="btn btn-primary mt-2">
                    Submit
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import $ from 'jquery'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Swal from 'sweetalert2'

export default {
  name: 'AddToNewVaultModal',
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      newVault: {},
      newVK: {}
    })
    return {
      state,
      async addKeepToNewVault() {
        try {
          const vaultId = await vaultsService.createVault(state.newVault)
          state.newVK.vaultID = vaultId
          state.newVK.keepId = props.keep.id
          await vaultKeepsService.create(state.newVK)
          $('#addToNewVault').modal('hide')
          Swal.fire({
            icon: 'success',
            text: 'Vault Created and Keep Added Successfully!'
          })
          router.push({ name: 'VaultDetailsPage', params: { id: vaultId } })
          state.newVault = {}
          state.newVK = {}
        } catch (error) {
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>
