import { useEffect, useState } from "react";
import { getStats, getWidgets, type Widget, type WidgetStats } from "./api";

export default function App() {
  const [widgets, setWidgets] = useState<Widget[]>([]);
  const [stats, setStats] = useState<WidgetStats | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    Promise.all([getWidgets(), getStats()])
      .then(([w, s]) => {
        setWidgets(w);
        setStats(s);
      })
      .catch((e: unknown) => setError(e instanceof Error ? e.message : String(e)));
  }, []);

  return (
    <main style={{ fontFamily: "system-ui, sans-serif", maxWidth: 640, margin: "3rem auto", padding: "0 1rem" }}>
      <h1>Omne — Inventory</h1>
      <p style={{ color: "#666" }}>
        Minimal screening UI. Data is fetched from <code>/api/widgets</code> via the BFF.
      </p>

      {error && (
        <p style={{ color: "#b23b3b" }}>
          Could not reach the API through the BFF: {error}
        </p>
      )}

      {stats && (
        <p>
          <strong>{stats.count}</strong> widget types · <strong>{stats.totalQuantity}</strong> total in stock
        </p>
      )}

      <ul>
        {widgets.map((w) => (
          <li key={w.id}>
            {w.name} — {w.quantity}
          </li>
        ))}
      </ul>
    </main>
  );
}
