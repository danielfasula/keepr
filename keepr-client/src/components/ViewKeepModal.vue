<template>
  <div class="view-keep-modal">
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
          <div class="modal-header">
            <h5 class="modal-title">File</h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ViewKeepModal',
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>
