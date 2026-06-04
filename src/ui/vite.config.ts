import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// The UI calls the API with relative `/api/*` paths so it works the same in
// dev and in a container. In dev, Vite proxies those calls to the BFF (which
// in turn forwards to the API). In a container, your nginx/Dockerfile is
// expected to serve the static build and route `/api` to the BFF.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      "/api": {
        target: process.env.BFF_URL ?? "http://localhost:5100",
        changeOrigin: true,
      },
    },
  },
});
