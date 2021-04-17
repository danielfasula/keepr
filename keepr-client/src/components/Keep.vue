<template>
  <div class="card p-3">
    <img class="card-img" :src="keep.img" />
    <div class="card-img-overlay d-flex align-items-end">
      <div class="row">
        <div class="col-12 d-flex justify-content-between">
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

  <!-- <div class="card-body">
    <h3 class="card-title">
      {{ keep.name }}
    </h3>
    <i
      class="fa fa-camera mr-2 mt-2 text-primary"
      data-toggle="modal"
      :data-target="'#view-keep-' + keep.id"
      id="hover-button"
      aria-hidden="true"
      @click="getKeep"
    ></i>
  </div>
  <img :src="keep.img" class="card-img-bottom" />
  <ViewKeepModal :keep="keep" /> -->
</template>

<script>
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Keep',
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      async getKeep() {
        await keepsService.getKeep(props.keep.id)
        await accountService.getVaults()
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
