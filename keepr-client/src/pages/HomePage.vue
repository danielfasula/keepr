<template>
  <div class="container-fluid">
    <div v-if="state.user.isAuthenticated" class="row">
      <div class="col">
        <button
          class="btn btn-warning"
          type="button"
          id="hover-button"
          data-toggle="modal"
          data-target="#addKeep"
          aria-hidden="true"
        >
          <i class="fa fa-plus-circle" aria-hidden="true"></i>
        </button>
      </div>
    </div>
    <AddKeepModal />
    <AddVaultModal />
    <div class="row stop">
      <div class="masonry-columns mt-5">
        <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async () => {
      await keepsService.getAllKeeps()
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
</style>
