import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Plus, Edit, Eye, Trash2, Users, Clock } from "lucide-react";

export function JobManagement() {
  const jobs = [
    { id: 1, title: "Senior React Developer", department: "Engineering", applicants: 45, status: "Active", posted: "5 days ago" },
    { id: 2, title: "Full Stack Engineer", department: "Engineering", applicants: 32, status: "Active", posted: "12 days ago" },
    { id: 3, title: "Product Manager", department: "Product", applicants: 28, status: "On Hold", posted: "20 days ago" },
    { id: 4, title: "UX Designer", department: "Design", applicants: 52, status: "Filled", posted: "30 days ago" },
  ];

  return (
    <DashboardLayout role="recruiter">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-white mb-2">Job Management</h1>
          <p className="text-gray-400">Create and manage job postings</p>
        </div>
        <Button>
          <Plus className="mr-2 h-4 w-4" />
          Create Job
        </Button>
      </div>

      <div className="space-y-4">
        {jobs.map((job) => (
          <GlassCard key={job.id} className="p-6">
            <div className="flex items-start justify-between">
              <div className="flex-1">
                <div className="flex items-center space-x-3 mb-3">
                  <h3 className="text-xl font-semibold text-white">{job.title}</h3>
                  <Badge
                    variant={job.status === "Active" ? "default" : job.status === "Filled" ? "secondary" : "outline"}
                  >
                    {job.status}
                  </Badge>
                </div>
                <div className="flex items-center space-x-6 text-sm text-gray-400 mb-4">
                  <span>{job.department}</span>
                  <div className="flex items-center space-x-1">
                    <Users className="h-4 w-4" />
                    <span>{job.applicants} applicants</span>
                  </div>
                  <div className="flex items-center space-x-1">
                    <Clock className="h-4 w-4" />
                    <span>Posted {job.posted}</span>
                  </div>
                </div>
                <div className="flex items-center space-x-3">
                  <Button variant="outline" size="sm">
                    <Eye className="mr-2 h-4 w-4" />
                    View Details
                  </Button>
                  <Button variant="outline" size="sm">
                    <Edit className="mr-2 h-4 w-4" />
                    Edit
                  </Button>
                  <Button variant="outline" size="sm">
                    <Users className="mr-2 h-4 w-4" />
                    View Candidates
                  </Button>
                  <Button variant="outline" size="sm">
                    <Trash2 className="h-4 w-4" />
                  </Button>
                </div>
              </div>
            </div>
          </GlassCard>
        ))}
      </div>
    </DashboardLayout>
  );
}
