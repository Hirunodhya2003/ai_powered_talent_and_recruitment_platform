import { useState } from "react";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Progress } from "../../components/ui/progress";
import { AlertCircle, CheckCircle2, Download, FileText, Trash2, Upload, Eye, RefreshCcw } from "lucide-react";

export function ResumeManagement() {
  const [fileMessage, setFileMessage] = useState("PDF or DOCX, maximum 5MB");
  const validate = (file?: File) => {
    if (!file) return;
    const valid = ["application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"].includes(file.type) || /\.(pdf|docx)$/i.test(file.name);
    if (!valid) return setFileMessage("Invalid format. Upload PDF or DOCX only.");
    if (file.size > 5 * 1024 * 1024) return setFileMessage("File exceeds 5MB maximum size.");
    setFileMessage(`${file.name} validated and ready to upload.`);
  };

  return (
    <DashboardLayout role="jobseeker">
      <div className="space-y-6">
        <div><p className="text-sm text-[#D4AF37]">Dashboard / Resume Manager</p><h1 className="mt-2 text-3xl font-semibold text-white">Resume Manager</h1><p className="text-gray-400">Upload, update, analyze, and manage the active resume used in applications.</p></div>
        <div className="grid xl:grid-cols-[.9fr_1.1fr] gap-6">
          <GlassCard className="p-6">
            <h2 className="text-xl font-semibold text-white mb-4">Upload Resume</h2>
            <label className="flex min-h-52 cursor-pointer flex-col items-center justify-center rounded-2xl border border-dashed border-[#D4AF37]/35 bg-[#2D2D2D]/35 p-8 text-center hover:bg-[#D4AF37]/5 transition-colors">
              <Upload className="h-10 w-10 text-[#D4AF37] mb-3" />
              <span className="text-white font-medium">Drop your resume here or browse</span>
              <span className="text-sm text-gray-400 mt-2">Supported formats: PDF, DOCX</span>
              <input type="file" className="hidden" accept=".pdf,.docx" onChange={(e) => validate(e.target.files?.[0])} />
            </label>
            <div className="mt-4 flex items-center gap-2 text-sm"><AlertCircle className="h-4 w-4 text-[#D4AF37]" /><span className="text-gray-300">{fileMessage}</span></div>
            <Button className="mt-5 w-full"><Upload className="mr-2 h-4 w-4" />Upload Resume</Button>
          </GlassCard>

          <GlassCard className="p-6">
            <div className="flex items-start justify-between gap-4"><div><h2 className="text-xl font-semibold text-white">Current Resume</h2><p className="text-gray-400 text-sm">Primary resume for job applications and AI analysis.</p></div><Badge className="bg-green-500/15 text-green-400 border-green-500/30">Active</Badge></div>
            <div className="mt-6 rounded-2xl border border-[#D4AF37]/10 bg-[#2D2D2D]/45 p-5 flex gap-4"><div className="rounded-xl bg-[#D4AF37]/10 p-4 h-fit"><FileText className="h-8 w-8 text-[#D4AF37]" /></div><div className="flex-1 grid sm:grid-cols-2 gap-4 text-sm"><Info label="Resume Name" value="Kavishi_Rajasekara_CV.pdf" /><Info label="Uploaded Date" value="18/06/2026" /><Info label="Last Updated Date" value="18/06/2026" /><Info label="Resume Status" value="Active" /></div></div>
            <div className="mt-5 grid sm:grid-cols-2 lg:grid-cols-4 gap-3"><Button variant="outline"><Eye className="mr-2 h-4 w-4" />View</Button><Button variant="outline"><Download className="mr-2 h-4 w-4" />Download</Button><Button variant="outline"><RefreshCcw className="mr-2 h-4 w-4" />Replace</Button><Button variant="ghost" className="text-red-400"><Trash2 className="mr-2 h-4 w-4" />Delete</Button></div>
          </GlassCard>
        </div>

        <GlassCard className="p-6">
          <h2 className="text-xl font-semibold text-white mb-6">AI Resume Analysis</h2>
          <div className="grid md:grid-cols-3 gap-6">
            <Score title="Resume Score" value={85} />
            <Score title="Profile Completeness" value={90} />
            <div className="rounded-2xl border border-[#D4AF37]/10 bg-[#2D2D2D]/45 p-5"><h3 className="text-white font-medium mb-3">Suggested Skills</h3><div className="flex flex-wrap gap-2"><Badge className="bg-[#D4AF37]/15 text-[#D4AF37]">React</Badge><Badge className="bg-[#D4AF37]/15 text-[#D4AF37]">ASP.NET Core</Badge><Badge className="bg-[#D4AF37]/15 text-[#D4AF37]">Azure</Badge></div></div>
          </div>
          <div className="mt-6 grid lg:grid-cols-2 gap-4">
            {["Add measurable project outcomes to your internship experience.", "Move React and ASP.NET Core into the top skills section.", "Include GitHub and portfolio links in the header.", "Tailor summary toward Software Engineer Intern roles."].map((x) => <div key={x} className="flex gap-3 rounded-xl bg-[#2D2D2D]/45 border border-[#D4AF37]/10 p-4"><CheckCircle2 className="h-5 w-5 text-[#D4AF37] shrink-0" /><span className="text-gray-300 text-sm">{x}</span></div>)}
          </div>
        </GlassCard>
      </div>
    </DashboardLayout>
  );
}
function Info({ label, value }: { label: string; value: string }) { return <div><p className="text-gray-500 text-xs uppercase tracking-[0.18em]">{label}</p><p className="text-white mt-1">{value}</p></div>; }
function Score({ title, value }: { title: string; value: number }) { return <div className="rounded-2xl border border-[#D4AF37]/10 bg-[#2D2D2D]/45 p-5"><div className="flex justify-between mb-3"><h3 className="text-white font-medium">{title}</h3><span className="text-[#D4AF37] font-semibold">{value}%</span></div><Progress value={value} /></div>; }
