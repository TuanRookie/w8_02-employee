const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    port: 3030 // Thay đổi cổng thành cổng bạn muốn sử dụng
  }
})
