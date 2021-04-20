<template>
  <div class="box keep p-0">
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
          <i class="fas fa-user-alt shadow"></i>
        </router-link>
      </div>
    </div>
  </div>
  <ViewKeepModal :keep="keep" />
  <AddToNewVaultModal :keep="keep" />
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
.keep:hover {
  transform: translateY(-10px);
}
.keep {
  transition: all 0.25s;
}
h5 {
  color: white;
  text-shadow: 2px 2px black;
}
.shadow {
  text-shadow: -1px 0 white, 0 1px white, 1px 0 white, 0 -1px white;
}
</style>
