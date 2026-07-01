import { Button } from "../components/ui/button";
import { GlassCard } from "../components/GlassCard";
import { useNavigate } from "react-router";
import { Home, ArrowLeft, SearchX } from "lucide-react";

export function NotFound() {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-[#0F0F0F] flex items-center justify-center p-4">
      <GlassCard className="p-12 max-w-2xl text-center">
        <div className="rounded-full bg-[#D4AF37]/10 p-6 w-fit mx-auto mb-6">
          <SearchX className="h-24 w-24 text-[#D4AF37]" />
        </div>
        <h1 className="text-6xl font-bold text-white mb-4">404</h1>
        <h2 className="text-2xl font-semibold text-white mb-4">Page Not Found</h2>
        <p className="text-gray-400 mb-8 text-lg">
          The page you're looking for doesn't exist or has been moved.
        </p>
        <div className="flex items-center justify-center space-x-4">
          <Button onClick={() => navigate(-1)}>
            <ArrowLeft className="mr-2 h-4 w-4" />
            Go Back
          </Button>
          <Button variant="outline" onClick={() => navigate("/")}>
            <Home className="mr-2 h-4 w-4" />
            Home Page
          </Button>
        </div>
      </GlassCard>
    </div>
  );
}
