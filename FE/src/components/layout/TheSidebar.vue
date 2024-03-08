<template>
  <div :class="{'zoomSidebar':!zoomSidebar}" class="wrapper__sidebar">
    <div :class="{'wrapper__sidebar-logo-zoom':!zoomSidebar}" class="wrapper__sidebar-logo">
      <div v-if="zoomSidebar" class="icon icon-sidebar icon-header-menu"></div>
      <div class="icon icon-sidebar icon-header-kt"></div>
      <div v-if="zoomSidebar" class="name-content-sidebar">{{ SideBarItem.accountant }}</div>
    </div>
    <div class="wrapper__sidebar-menu">
      <router-link to="/employee" style="text-decoration: none">
        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-home"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.employee }}</div>
        </a>
      </router-link>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-cash"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.cash }}</div>
        </a>
      
        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-bank"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.bank }}</div>
        </a>
      
        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-buy"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.buy }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-sale"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.sale }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-invoice"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.invoice }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-stock"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.stock }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-tools"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.tools }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-fixed-assets"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.fixedAssets }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-tax"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.tax }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-price"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.price }}</div>
        </a>

        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-general"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.general }}</div>
        </a>

      <router-link to="/report" style="text-decoration: none">
        <a href="/report" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-report"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.report }}</div>
        </a>
      </router-link>

      
        <a href="" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-customer"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.customer }}</div>
        </a>
      

      <router-link to="statistics" style="text-decoration: none">
        <a href="/statistics" class="menu-item">
          <div class="item-icon">
            <div class="icon icon-statistics"></div>
          </div>
          <div v-if="zoomSidebar" class="item-title">{{ SideBarItem.statistics }}</div>
        </a>
      </router-link>
      <!-- <div
    @click="handleInSidebar"
    v-else
    class="sidebar__item-mr"
    >
      <div class="icon-sidebar-mr" ></div>
  </div> -->
    </div>
    <div v-if="zoomSidebar" class="sidebar__item--tg" @click="handleOutSidebar">
      <div class="icon icon-tg"></div>
      <div class="item-title">Thu gọn</div>
    </div>
    <div @click="handleInSidebar" v-else class="sidebar__item--mr">
      <div class="icon icon-mr"></div>
    </div>
  </div>
</template>

<script>
import RResource from "../../js/RResource"
import { mapActions, mapGetters } from "vuex";
export default {
  name: "SidebarComponent",
  computed: mapGetters(["zoomSidebar"]),
  data() {
        return {
            SideBarItem:RResource.SidebarItem
        }
    },
  methods: {
    ...mapActions(["toggleZoomSidebar"]),
    /**
     * zoom Out Sidebar
     * Author:dcTuan (28/11/2023)
     * Modify:null
     */
    handleOutSidebar() {
      this.toggleZoomSidebar();
    },
    /**
     * zoom Out Sidebar
     * Author:dcTuan (28/11/2023)
     * Modify:null
     */
    handleInSidebar() {
      this.toggleZoomSidebar();
    },
  },
};
</script>
