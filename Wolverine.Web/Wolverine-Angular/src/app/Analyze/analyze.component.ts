import { Component, HostListener } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, SortSession, SortResult } from '../Contracts/Contracts';
import { ClipboardService } from 'ngx-clipboard';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'analyze',
  templateUrl: './analyze.component.html'
})
export class AnalyzeComponent {

  ActiveSortResult: SortResult;
  ActiveProject: Project;

  constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute) {
    // Reserved. 
  }

  ngOnInit() {
    var id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    this.projectService.loadResult(id).subscribe((sortResult: SortResult) =>
    {
      console.log(sortResult);
      this.ActiveSortResult = sortResult;
      this.ActiveProject = sortResult.referencedProject;
    });
  }
}