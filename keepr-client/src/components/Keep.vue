<template>
  <div class="Keep card p-3 border-rounded">
    <div class="card-body">
      <h3 class="card-title">
        {{ keep.name }}
      </h3>
      <i
        class="fa fa-camera mr-2 mt-2 text-primary"
        data-toggle="modal"
        :data-target="'#view-keep-' + keep.id"
        id="hover-button"
        aria-hidden="true"
      ></i>
    </div>
    <img :src="keep.img" class="card-img-bottom" />
    <ViewKeepModal :keep="keep" />
  </div>
</template>

<script>
export default {
  name: 'Keep',
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>
