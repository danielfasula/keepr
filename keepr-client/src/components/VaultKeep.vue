<template>
  <div class="card p-3">
    <img class="card-img" :src="keep.img" />
    <div class="card-img-overlay d-flex">
      <div class="row">
        <div class="col-12">
          <i
            type="button"
            class="fas fa-minus-circle"
            @click="removeFromVault"
          ></i>
        </div>
      </div>
      <div class="row align-items-end">
        <div class="col-12 d-flex">
          <h5
            data-toggle="modal"
            :data-target="'#view-keep-' + keep.id"
            id="hover"
            aria-hidden="true"
            @click="getKeep"
          >
            {{ keep.name }}
          </h5>
          <ViewKeepModal :keep="keep" />
          <router-link
            :to="{ name: 'ProfilePage', params: { id: keep.creatorId } }"
          >
            <i class="fas fa-user-alt"></i>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Swal from 'sweetalert2'

export default {
  name: 'VaultKeep',
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      async getKeep() {
        await keepsService.getKeep(props.keep.id)
        await accountService.getVaults()
      },
      async removeFromVault() {
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
            await vaultKeepsService.removeKeepFromVault(props.keep.vaultKeepId)
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

<style scoped>
.card-img-overlay {
  padding: 0rem !important;
}

#hover:hover {
  cursor: pointer;
}
</style>
