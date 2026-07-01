import { useNavigate } from "react-router";
import { ShieldAlert, Sparkles } from "lucide-react";
import { Button } from "../components/ui/button";
import { GlassCard } from "../components/GlassCard";
import { getCurrentRole, dashboardByRole } from "../auth";

export function AccessDenied() {
  const navigate = useNavigate();
  const role = getCurrentRole();
  return (
    <div className="min-h-screen bg-[#0F0F0F] flex items-center justify-center p-6">
      <GlassCard className="max-w-xl p-10 text-center">
        <Sparkles className="h-10 w-10 text-[#D4AF37] mx-auto mb-4" />
        <ShieldAlert className="h-16 w-16 text-red-400 mx-auto mb-5" />
        <h1 className="text-3xl font-semibold text-white mb-3">Access Denied</h1>
        <p className="text-gray-400 mb-8">Your current role does not have permission to open this workspace. TalentAI enforces separate portals for Job Seekers, Recruiters, Hiring Managers, and Administrators.</p>
        <div className="flex flex-col sm:flex-row gap-3 justify-center">
          <Button onClick={() => navigate(role ? dashboardByRole[role] : "/login")}>Go to my dashboard</Button>
          <Button variant="outline" onClick={() => navigate("/login")}>Sign in as another user</Button>
        </div>
      </GlassCard>
    </div>
  );
}
