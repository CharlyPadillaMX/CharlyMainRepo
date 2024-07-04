import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from "vite";
import vue from '@vitejs/Plugin-vue';

export default defineConfig(({ mode }) => ({
    Plugins: [
        vue(),
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    build: {
      outDir: mode === 'production' ? 'dist-prod' : 'dist'
    }
}));
