<template>
  <div class="card-columns mt-5">
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
@media (max-width: 767px) {
  .card-columns {
    column-count: 2;
    margin-right: 10px;
    margin-left: 10px;
  }
}
@media (min-width: 767.98px) {
  .card-columns {
    column-count: 4;
    margin-right: 30px;
    margin-left: 30px;
  }
}
</style>
