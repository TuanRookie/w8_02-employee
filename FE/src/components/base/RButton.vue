<template>
    <button :class="classButton">
        {{ contentButton }}
    </button>
</template>

<script>
export default{
    name:"RButton",
    props:{
        classButton:{
            type: String,
            default: "",
        },
        contentButton:{
            type:String,
            default: "",
        },
    }
}

</script>
<style scoped>
@import url(../../css/base/button.css);
</style>