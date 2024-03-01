<template>
  <p style="font-size: 14px">
    {{ text.selectValueToImport }}
  </p>
  <div class="inputfile-box">
    <input type="file" id="file" class="inputfile" @change="uploadFile" />
    <label for="file" class="text-file">
      <span id="file-name" class="file-box">{{ text.selectAFile }}</span>
      <span class="file-button">{{ text.select }}</span>
    </label>
  </div>
  <p style="font-size: 14px">
    {{ text.noFileTemplate }}
    <a class="link-example" href="#">{{ text.here }}</a>
  </p>
</template>
<script>
import { mapActions, mapGetters } from "vuex";
import RResource from "../../js/RResource";
export default {
  name: "ImportSelectFile",
  computed: mapGetters(["importEmployee"]),
  data() {
    return {
      text: RResource.Text,
    };
  },

  props: {
    modelValue: {
      type: [String, File],
      require: false,
    },
  },
  methods: {
    ...mapActions(["setFileImport"]),
    /**
     * Thay đổi file
     * @param {event} e
     * Author: DCTuan(18/01/2024)
     */
    uploadFile(e) {
      this.setFileImport(e.target.files[0]);
    },
  },
};
</script>
<style scoped>
.inputfile-box {
  position: relative;
}
.inputfile {
  display: none;
}

.container {
  display: inline-block;
  width: 100%;
}
.text-file {
  margin-top: 12px;
  margin-bottom: 12px;
  display: flex;
  column-gap: 20px;
}
.file-box {
  display: inline-block;
  width: 70%;
  border: 1px solid #a49d9d;
  border-radius: 4px;
  padding: 0 12px;
  line-height: 36px;
  height: 36px;
  box-sizing: border-box;
  font-size: 14px;
}
.file-name {
  font-size: 14px;
}
.file-button {
  min-width: 100px;
  height: 36px;
  line-height: 36px;
  background-color: #50b83c;
  color: #ffffff;
  font-size: 14px;
  font-weight: 500;
  border: unset;
  cursor: pointer;
  text-align: center;
  border-radius: 4px;
  color: #1f1f1f;
  border: 1px solid #a49d9d;
  background-color: #ffffff;
}
.link-example {
  text-decoration: none;
  color: #0f84d2;
}
</style>
