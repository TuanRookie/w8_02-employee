<template>
  <BasePaging
    :recordList="employees"
    :pageNumber="filter.pageNumber"
    :pageSize="filter.pageSize"
    :totalRecord="totalEmployee"
    :totalPage="totalPage"
    @selectPageNumber="selectPageNumber"
    @selectPageSize="selectPageSize"
  />
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import BasePaging from "../../components/base/basePaging/BasePaging.vue";

export default {
  name: "EmployeePaging",
  components: { BasePaging },
  computed: mapGetters(["totalEmployee", "totalPage", "filter", "employees"]),
  methods: {
    ...mapActions(["setFilter", "getEmployee"]),

    /**
     * chọn số trang
     * @param {int} pageNumber
     * Author: DCTuan (15/01/2024)
     */
    selectPageNumber(pageNumber) {
      this.setFilter({
        pageSize: this.filter.pageSize,
        pageNumber: pageNumber,
        employeeFilter: this.filter.employeeFilter,
      });
      this.getEmployee();
    },

    /**
     * chọn số lượng bản ghi mỗi trang
     * @param {int} pageSize
     * Author: DCTuan (15/01/2024)
     */
    selectPageSize(pageSize) {
      pageSize = pageSize.split(" ")[0];
      this.setFilter({
        pageSize: pageSize,
        pageNumber: 1,
        employeeFilter: this.filter.employeeFilter,
      });
      this.getEmployee();
    },
  },
};
</script>
