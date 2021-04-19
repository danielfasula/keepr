<template>
  <div class="add-keep-modal container-fluid">
    <div
      class="modal fade"
      id="addKeep"
      tabindex="-1"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">New Keep</h5>
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
            <form @submit.prevent="createKeep">
              <div class="form-group">
                <label for="title"> Title </label>
                <input
                  type="text"
                  class="form-control"
                  name="title"
                  id="title"
                  placeholder="Title..."
                  v-model="state.newKeep.name"
                />
              </div>
              <div class="form-group">
                <label for="imgurl"> Image URL </label>
                <input
                  type="text"
                  class="form-control"
                  name="imgUrl"
                  id="imgUrl"
                  placeholder="URL..."
                  v-model="state.newKeep.img"
                />
              </div>
              <div class="form-group">
                <label for="description"> Description </label>
                <textarea
                  class="form-control"
                  name="description"
                  id="description"
                  placeholder="Keep Description..."
                  v-model="state.newKeep.description"
                  rows="3"
                ></textarea>
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
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
// import { useRoute } from 'vue-router'
export default {
  name: 'AddKeepModal',
  props: [],
  setup() {
    // const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          $('#addKeep').modal('hide')
          // if (route.name === 'account') {
          //   AppState.userKeeps.push(res.data)
          // }
          state.newKeep = {}
        } catch (error) {
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style scoped>
label {
  margin-bottom: 0rem !important;
}
</style>
