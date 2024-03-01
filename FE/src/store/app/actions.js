const actions = {

    toggleZoomSidebar(context,zoom){
        context.commit("toggleZoomSidebar",zoom);
    },
    /**
     * Ẩn/hiện loading table
     * @param {*} context 
     * Author:DcTuan (09/01/2024)
     */
    toggleLoading(context){
        context.commit("toggleLoading");
    },

    /**
     * Ẩn/hiện loading 
     * @param {*} context 
     * Author:DcTuan (09/01/2024)
     */
    toggleLoad(context){
        context.commit("toggleLoad");
    },

    /**
     * Ẩn/hiện dialog
     * @param {*} context 
     * Author:DcTuan (09/01/2024)
     */
    toggleDialog(context) {
        context.commit("toggleDialog");
    },

    /**
     * Ẩn/hiện cảnh báo
     * @param {*} context 
     * Author:DcTuan (09/01/2024)
     */
    toggleAlert(context) {
        context.commit("toggleAlert");
    },

    /**
     * Xử lí nội dung của cảnh báo
     * @param {*} context 
     * @param {*} alert 
     * Author:DcTuan (09/01/2024)
     */
    setDialog(context,alert){
        context.commit("setDialog",alert);
        context.dispatch("toggleDialog");
        console.log("check",alert)
    },

    /**
     * Xử lí nội dung của cảnh báo
     * @param {*} context 
     * @param {*} alert 
     * Author:DcTuan (09/01/2024)
     */
    setAlert(context,alert){
        context.commit("setAlert",alert);
        context.dispatch("toggleAlert");
    },

}

export default actions