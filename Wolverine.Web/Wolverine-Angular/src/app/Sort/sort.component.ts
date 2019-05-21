import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card } from '../Contracts/Contracts';
import { ClipboardService } from 'ngx-clipboard';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'sort',
  templateUrl: './sort.component.html'
})
export class SortComponent {

  ActiveProject: Project;
  Id: string;
  IsSaving: boolean;
  IsSavingSuccessful: boolean;
  IsDirty: boolean;

  constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute, private clipboardService: ClipboardService) {
    // Reserved. 
  }

  ngOnInit() {
      this.Id = this.route.snapshot.paramMap.get('id');
      this.projectService.load(this.Id).subscribe((project: Project) => {
          this.ActiveProject = project;
      });
  }
}