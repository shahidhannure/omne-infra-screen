export interface Widget {
  id: number;
  name: string;
  quantity: number;
}

export interface WidgetStats {
  count: number;
  totalQuantity: number;
}

// Relative paths on purpose: the serving layer (Vite dev proxy, or your
// nginx/Dockerfile in a container) is responsible for routing `/api` to the BFF.
export async function getWidgets(): Promise<Widget[]> {
  const res = await fetch("/api/widgets");
  if (!res.ok) throw new Error(`GET /api/widgets failed: ${res.status}`);
  return res.json();
}

export async function getStats(): Promise<WidgetStats> {
  const res = await fetch("/api/widgets/stats");
  if (!res.ok) throw new Error(`GET /api/widgets/stats failed: ${res.status}`);
  return res.json();
}
