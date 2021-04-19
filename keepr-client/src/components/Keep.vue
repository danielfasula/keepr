<template>
  <div class="box p-0">
    <img class="img-fluid" :src="keep.img" />
    <div class="row name-profile">
      <div class="col-12 d-flex justify-content-around">
        <h5
          data-toggle="modal"
          :data-target="'#view-keep-' + keep.id"
          id="hover"
          aria-hidden="true"
          @click="getKeep"
        >
          {{ keep.name }}
        </h5>
        <router-link
          :to="{ name: 'ProfilePage', params: { id: keep.creatorId } }"
        >
          <i class="fas fa-user-alt"></i>
        </router-link>
      </div>
    </div>
    <ViewKeepModal :keep="keep" />
  </div>
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
#hover:hover {
  cursor: pointer;
}

.name-profile {
  transform: translateY(-30px);
}

img {
  border-radius: 15px;
}
</style>
