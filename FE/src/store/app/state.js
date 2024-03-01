import DialogAction from "@/enums/dialogAction";
const state = {
    isShowLoading:false,
    isLoad: false,
    isShowAlert: false,
    isShowDialog: false,
    zoomSidebar: true,
    dialog:{
        type:"",
        title:"",
        message:"",
        action:DialogAction.DEFAULT
    },
    alert:{
        type:"",
        message:"",
    }
}
export default state