const mutations = {
    toggleLoading(state){
        state.isShowLoading=!state.isShowLoading;
    },
    toggleLoad(state){
        state.isLoad = !state.isLoad;
    },
    toggleDialog(state){
        state.isShowDialog=!state.isShowDialog;
    },
    toggleAlert(state){
        state.isShowAlert=!state.isShowAlert;
    },
    setDialog(state,payload){
        state.dialog = payload;
    },
    setAlert(state,payload){
        state.alert = payload;
    },
    toggleZoomSidebar(state) {
        state.zoomSidebar = !state.zoomSidebar;
    },
}
export default mutations