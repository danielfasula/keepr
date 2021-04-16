<template>
  <div class="card-columns">
    <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
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
