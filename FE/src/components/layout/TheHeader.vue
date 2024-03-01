<template>
  <div class="header">
    <div class="wrapper__container--left">
      <div class="icon icon-three-stripes"></div>
      <div class="navbar-branch">
        <div class="branch-name">
          CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
        </div>
        <div class="icon icon-chevron-down-bold"></div>
      </div>
    </div>
    <div class="wrapper__container--right">
      <div class="icon icon-bell"></div>
      <div class="navbar-user">
        <div class="user-image">
          <img src="../../assets/img/avatar.jpg" alt="" />
        </div>
        <div class="user-name">Đinh Công Tuấn</div>
        <div
          class="icon icon-chevron-down"
          @click="this.isShowActionUser = !this.isShowActionUser"
        >
          <div v-if="isShowActionUser" class="user-action">
            <div class="user-action-item">Thông tin cá nhân</div>
            <div class="user-action-item" @click="logOutSystem">Đăng xuất</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "HeaderComponent",
  data() {
    return {
      isShowActionUser: false,
    };
  },
  methods: {
    logOutSystem() {
      console.log("check");
      localStorage.setItem("Token", JSON.stringify(null));
      localStorage.setItem("User", JSON.stringify(null));

      this.$router.push("/login").finally(() => {
        document.title = "Đăng nhập"; // Đặt lại tiêu đề sau khi chuyển hướng
        location.reload();
      });
      
    },
  },
};
</script>
