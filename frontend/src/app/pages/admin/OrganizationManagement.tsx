import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Building, Users, Briefcase, Plus, Edit, Eye } from "lucide-react";

export function OrganizationManagement() {
  const organizations = [
    { id: 1, name: "Tech Innovations Inc.", users: 24, jobs: 12, status: "Active", type: "Enterprise" },
    { id: 2, name: "StartupXYZ", users: 8, jobs: 6, status: "Active", type: "Startup" },
    { id: 3, name: "Digital Solutions", users: 15, jobs: 9, status: "Active", type: "Agency" },
  ];

  return (
    <DashboardLayout role="admin">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-white mb-2">Organization Management</h1>
          <p className="text-gray-400">Manage companies and departments</p>
        </div>
        <Button>
          <Plus className="mr-2 h-4 w-4" />
          Add Organization
        </Button>
      </div>

      <div className="space-y-4">
        {organizations.map((org) => (
          <GlassCard key={org.id} className="p-6">
            <div className="flex items-start justify-between">
              <div className="flex items-start space-x-4">
                <div className="rounded-lg bg-[#D4AF37]/10 p-3">
                  <Building className="h-6 w-6 text-[#D4AF37]" />
                </div>
                <div>
                  <div className="flex items-center space-x-2 mb-2">
                    <h3 className="text-xl font-semibold text-white">{org.name}</h3>
                    <Badge variant="outline">{org.type}</Badge>
                    <Badge className="bg-green-500/20 text-green-500 border-green-500/40">
                      {org.status}
                    </Badge>
                  </div>
                  <div className="flex items-center space-x-6 text-sm text-gray-400">
                    <div className="flex items-center space-x-1">
                      <Users className="h-4 w-4" />
                      <span>{org.users} users</span>
                    </div>
                    <div className="flex items-center space-x-1">
                      <Briefcase className="h-4 w-4" />
                      <span>{org.jobs} active jobs</span>
                    </div>
                  </div>
                </div>
              </div>
              <div className="flex items-center space-x-2">
                <Button variant="outline" size="sm">
                  <Eye className="mr-2 h-4 w-4" />
                  View
                </Button>
                <Button variant="outline" size="sm">
                  <Edit className="mr-2 h-4 w-4" />
                  Edit
                </Button>
              </div>
            </div>
          </GlassCard>
        ))}
      </div>
    </DashboardLayout>
  );
}
