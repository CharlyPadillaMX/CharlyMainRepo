<template>
    <div>
        <h1>{{ part.title }}</h1>
        <di>
            {{ part.description }}
        </di>
    </div>
</template>

<script setup>
import { usePartsStore } from '@/stores/partsStore';
import { computed } from 'vue';


const partsStore = usePartsStore();
const props = defineProps(['partType', 'id']);
const part = computed(() => {
    return partsStore.parts[props.partType].find(part => part.id === +props.id);
});
</script>
