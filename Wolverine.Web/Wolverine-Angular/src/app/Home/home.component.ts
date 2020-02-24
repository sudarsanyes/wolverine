import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, Guid } from '../Contracts/Contracts';
import { Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  CreateProjectName: string = 'Untitled-1';
  SortProjectId: string;
  AnalyzeProjectId: string;
  NewProject: Project;
  EditProjectId: string;
  IsCreatingNewProject: boolean;
  IsOpeningForSorting: boolean;
  IsOpeningForAnalysis: boolean;
  IsOpeningForSortingFailed: boolean;
  IsOpeningExistingProject: boolean;
  IsCreateProjectFailed: boolean;
  IsOpeningForAnalysisFailed: boolean;
  IsSortProjectIdMissing: boolean;
  IsAnalyzeProjectIdMissing: boolean;

  constructor(private projectService: ProjectService, private router: Router) {
    this.IsCreatingNewProject = false;
    this.IsOpeningForSorting = false;
    this.IsOpeningForAnalysis = false;
    this.IsOpeningForSortingFailed = false;
    this.IsOpeningForAnalysisFailed = false;
    this.IsOpeningExistingProject = false;
    this.IsSortProjectIdMissing = false;
    this.IsAnalyzeProjectIdMissing = false;
    this.NewProject = new Project();
    this.NewProject.id = Guid.newGuid();
  }

  Sort() {
    if (this.SortProjectId == null) {
      this.IsSortProjectIdMissing = true;
    }
    else {
      this.IsSortProjectIdMissing = false;
      this.IsOpeningForSortingFailed = false;
      this.IsOpeningForSorting = true;
      this.projectService.createSort(this.SortProjectId).subscribe((sortSessionId: string) => {
        if (sortSessionId == null) {
          this.IsOpeningForSorting = false;
          this.IsOpeningForSortingFailed = true;
        }
        else {
          this.router.navigateByUrl('/sort/' + sortSessionId);
          this.IsOpeningForSorting = false;
        }
      });
    }
  }

  Create() {
    if (this.NewProject.name == null || this.NewProject.author == null) {
      console.log("missing fields");
      this.IsCreateProjectFailed = true;
    }
    else {
      console.log(this.NewProject);
      this.IsCreateProjectFailed = false;
      this.IsCreatingNewProject = true;
      this.projectService.create(this.NewProject).subscribe((id: string) => {
        this.router.navigateByUrl('/create/' + id);
        this.IsCreatingNewProject = false;
      });
    }
  }

  Open() {
    this.IsOpeningExistingProject = true;
    this.router.navigateByUrl('/create/' + this.EditProjectId);
    this.IsCreatingNewProject = false;
  }

  Analyze() {
    if (this.AnalyzeProjectId == null) {
      this.IsAnalyzeProjectIdMissing = true;
    }
    else {
      this.IsAnalyzeProjectIdMissing = false;
      this.projectService.load(this.AnalyzeProjectId).subscribe((project: Project) => {
        if (project == null || !project.isSessionZero || !project.isPublished) {
          this.IsOpeningForAnalysis = false;
          this.IsOpeningForAnalysisFailed = true;
        }
        else {
          this.router.navigateByUrl('/analyze/' + this.AnalyzeProjectId);
          this.IsOpeningForAnalysis = false;
        }
      });
    }
  }
}