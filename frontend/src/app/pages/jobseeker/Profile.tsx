import { useState, type ReactNode } from "react";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Input } from "../../components/ui/input";
import { Badge } from "../../components/ui/badge";
import { Textarea } from "../../components/ui/textarea";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../../components/ui/select";
import { Camera, CheckCircle2, Edit3, Plus, Save, Trash2, X } from "lucide-react";

type Item = { id: number; title: string; meta: string; extra?: string };
type Skill = { id: number; name: string; level: "Beginner" | "Intermediate" | "Advanced" };

export function JobSeekerProfile() {
  const [editing, setEditing] = useState(false);
  const [educations, setEducations] = useState<Item[]>([
    { id: 1, title: "University of Colombo School of Computing", meta: "BSc Software Engineering", extra: "2023 - 2027" },
  ]);
  const [skills, setSkills] = useState<Skill[]>([
    { id: 1, name: "React", level: "Advanced" },
    { id: 2, name: "ASP.NET Core", level: "Intermediate" },
    { id: 3, name: "SQL", level: "Intermediate" },
  ]);
  const [experience, setExperience] = useState<Item[]>([
    { id: 1, title: "Software Engineering Intern", meta: "Virtusa", extra: "Jan 2026 - Present" },
  ]);

  const addEducation = () => setEducations([...educations, { id: Date.now(), title: "New Institution", meta: "Degree / Qualification", extra: "2026 - 2027" }]);
  const addSkill = () => setSkills([...skills, { id: Date.now(), name: "New Skill", level: "Beginner" }]);
  const addExperience = () => setExperience([...experience, { id: Date.now(), title: "Job Title", meta: "Company Name", extra: "Duration" }]);

  return (
    <DashboardLayout role="jobseeker">
      <div className="space-y-6">
        <div className="flex items-end justify-between gap-4">
          <div>
            <p className="text-sm text-[#D4AF37]">Dashboard / My Profile</p>
            <h1 className="mt-2 text-3xl font-semibold text-white">My Profile</h1>
            <p className="text-gray-400">Manage your candidate identity, career story, skills, and open-to-work preferences.</p>
          </div>
          <div className="flex gap-3">
            {editing && <Button variant="outline" onClick={() => setEditing(false)}><X className="mr-2 h-4 w-4" />Cancel</Button>}
            <Button onClick={() => setEditing(!editing)}>{editing ? <Save className="mr-2 h-4 w-4" /> : <Edit3 className="mr-2 h-4 w-4" />}{editing ? "Save Changes" : "Edit"}</Button>
          </div>
        </div>

        <div className="grid xl:grid-cols-[1.4fr_.8fr] gap-6">
          <GlassCard className="p-6">
            <h2 className="text-xl font-semibold text-white mb-5">Personal Information</h2>
            <div className="flex flex-col lg:flex-row gap-6">
              <div className="flex flex-col items-center gap-3">
                <div className="h-28 w-28 rounded-full border border-[#D4AF37]/40 bg-gradient-to-br from-[#D4AF37]/30 to-[#2D2D2D] flex items-center justify-center text-3xl font-semibold text-[#D4AF37]">KR</div>
                <Button variant="outline" size="sm"><Camera className="mr-2 h-4 w-4" />Profile Picture</Button>
              </div>
              <div className="grid md:grid-cols-2 gap-4 flex-1">
                {["Full Name", "Email Address", "Phone Number", "Location"].map((label, index) => (
                  <div key={label}>
                    <label className="text-xs uppercase tracking-[0.18em] text-gray-500">{label}</label>
                    <Input disabled={!editing} className="mt-2 bg-[#2D2D2D] border-[#D4AF37]/20 text-white" defaultValue={["Kavishi Rajasekara", "kavishi.rajasekara@email.com", "+94 77 456 7890", "Colombo, Sri Lanka"][index]} />
                  </div>
                ))}
              </div>
            </div>
          </GlassCard>

          <GlassCard className="p-6 border-[#D4AF37]/30 bg-[#1E1E1E]/80">
            <div className="flex items-center gap-2 mb-4"><span className="h-3 w-3 rounded-full bg-green-500" /><h2 className="text-xl font-semibold text-white">Open To Work</h2></div>
            <div className="space-y-3 text-sm">
              {[ ["Currently Looking for a Job", "Yes"], ["Preferred Job Role", "Software Engineer Intern"], ["Employment Type", "Internship"], ["Preferred Work Location", "Colombo, Sri Lanka"], ["Work Preference", "Remote / Hybrid"] ].map(([k,v]) => (
                <div key={k} className="flex justify-between gap-4 border-b border-[#D4AF37]/10 pb-2"><span className="text-gray-400">{k}</span><span className="text-white text-right">{v}</span></div>
              ))}
            </div>
            <div className="mt-5 flex gap-3"><Button className="flex-1">Update Preferences</Button><Button variant="outline" className="flex-1">Save Changes</Button></div>
          </GlassCard>
        </div>

        <GlassCard className="p-6">
          <h2 className="text-xl font-semibold text-white mb-4">Professional Information</h2>
          <div className="grid lg:grid-cols-2 gap-4">
            <div><label className="text-xs uppercase tracking-[0.18em] text-gray-500">Professional Headline</label><Input className="mt-2 bg-[#2D2D2D] border-[#D4AF37]/20 text-white" defaultValue="Software Engineering Undergraduate" /></div>
            <div><label className="text-xs uppercase tracking-[0.18em] text-gray-500">Profile Completeness</label><div className="mt-2 rounded-lg bg-[#2D2D2D] p-3 text-[#D4AF37] font-semibold">90% complete</div></div>
            <div className="lg:col-span-2"><label className="text-xs uppercase tracking-[0.18em] text-gray-500">About Me / Summary</label><Textarea className="mt-2 bg-[#2D2D2D] border-[#D4AF37]/20 text-white min-h-28" defaultValue="Software Engineering Undergraduate with hands-on experience in React, ASP.NET Core, REST APIs, and database-backed enterprise applications." /></div>
          </div>
        </GlassCard>

        <Section title="Education" onAdd={addEducation} addLabel="Add Education">
          {educations.map((e) => <EditableRow key={e.id} title={e.title} meta={e.meta} extra={e.extra} onDelete={() => setEducations(educations.filter(x => x.id !== e.id))} />)}
        </Section>

        <GlassCard className="p-6">
          <div className="flex justify-between mb-5"><h2 className="text-xl font-semibold text-white">Skills</h2><Button onClick={addSkill}><Plus className="mr-2 h-4 w-4" />Add Skill</Button></div>
          <div className="grid md:grid-cols-2 xl:grid-cols-3 gap-3">
            {skills.map((skill) => <div key={skill.id} className="rounded-xl border border-[#D4AF37]/10 bg-[#2D2D2D]/50 p-4"><Input className="bg-[#1E1E1E] border-[#D4AF37]/20 text-white mb-3" value={skill.name} onChange={(e) => setSkills(skills.map(s => s.id === skill.id ? {...s, name: e.target.value} : s))} /><Select value={skill.level} onValueChange={(v: Skill['level']) => setSkills(skills.map(s => s.id === skill.id ? {...s, level: v} : s))}><SelectTrigger className="bg-[#1E1E1E] border-[#D4AF37]/20 text-white"><SelectValue /></SelectTrigger><SelectContent><SelectItem value="Beginner">Beginner</SelectItem><SelectItem value="Intermediate">Intermediate</SelectItem><SelectItem value="Advanced">Advanced</SelectItem></SelectContent></Select><Button variant="ghost" className="mt-3 text-red-400" onClick={() => setSkills(skills.filter(s => s.id !== skill.id))}><Trash2 className="mr-2 h-4 w-4" />Delete Skill</Button></div>)}
          </div>
        </GlassCard>

        <Section title="Experience" onAdd={addExperience} addLabel="Add Experience">
          {experience.map((e) => <EditableRow key={e.id} title={e.title} meta={e.meta} extra={e.extra} onDelete={() => setExperience(experience.filter(x => x.id !== e.id))} />)}
        </Section>

        <GlassCard className="p-6">
          <h2 className="text-xl font-semibold text-white mb-4">Job Preferences</h2>
          <div className="grid md:grid-cols-3 gap-4">
            <Input className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" defaultValue="Software Engineer Intern" />
            <Input className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" defaultValue="Colombo, Sri Lanka" />
            <Select defaultValue="internship"><SelectTrigger className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white"><SelectValue /></SelectTrigger><SelectContent><SelectItem value="internship">Internship</SelectItem><SelectItem value="fulltime">Full-Time</SelectItem><SelectItem value="parttime">Part-Time</SelectItem></SelectContent></Select>
          </div>
          <Button className="mt-5"><CheckCircle2 className="mr-2 h-4 w-4" />Save</Button>
        </GlassCard>
      </div>
    </DashboardLayout>
  );
}

function Section({ title, children, onAdd, addLabel }: { title: string; children: ReactNode; onAdd: () => void; addLabel: string }) {
  return <GlassCard className="p-6"><div className="flex justify-between mb-5"><h2 className="text-xl font-semibold text-white">{title}</h2><Button onClick={onAdd}><Plus className="mr-2 h-4 w-4" />{addLabel}</Button></div><div className="space-y-3">{children}</div></GlassCard>;
}

function EditableRow({ title, meta, extra, onDelete }: { title: string; meta: string; extra?: string; onDelete: () => void }) {
  return <div className="flex flex-col lg:flex-row lg:items-center justify-between gap-4 rounded-xl border border-[#D4AF37]/10 bg-[#2D2D2D]/45 p-4"><div><h3 className="text-white font-medium">{title}</h3><p className="text-gray-400 text-sm">{meta}</p>{extra && <Badge variant="outline" className="mt-2 border-[#D4AF37]/30 text-[#D4AF37]">{extra}</Badge>}</div><div className="flex gap-2"><Button variant="outline" size="sm"><Edit3 className="mr-2 h-4 w-4" />Edit</Button><Button variant="ghost" size="sm" className="text-red-400" onClick={onDelete}><Trash2 className="mr-2 h-4 w-4" />Delete</Button></div></div>;
}
