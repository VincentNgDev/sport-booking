"use client";

import ResourceScheduler from "@/components/apps/app-calendar/resource-scheduler/resource-scheduler";
import { ResourceSchedulerProvider } from "@/components/apps/app-calendar/resource-scheduler/resource-scheduler-provider";
import AppDatePicker from "@/components/apps/app-datetime-picker/app-date-picker";
import {
  AppForm,
  AppFormDataTable,
  AppFormFieldInput,
  AppFormFieldWrapper,
  AppFormSubmit,
} from "@/components/apps/app-form/app-form";
import { ThemeSwitch } from "@/components/providers/theme-provider";
import { buttonVariants } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { Trash2 } from "lucide-react";
import React from "react";

const testData = {
  input1: "Initial Values",
  data: [
    // { id: 1, name: "Item 1", description: "Description 1" },
    // { id: 2, name: "Item 2", description: "Description 2" },
    // { id: 3, name: "Item 3", description: "Description 3" },
  ],
};

export default function LandingPage() {
  return (
    <div className="flex min-h-screen flex-col w-full">
      <ThemeSwitch />
      <AppDatePicker />
      <AppForm
        initialValue={testData}
        captionSpan={3}
        onformSubmit={(values) => {
          console.log("Form submitted with values:", values);
          return {
            success: true,
            message: "Form submitted successfully!",
          };
        }}
      >
        <AppFormFieldInput
          propertyName="input1"
          isRequiredField={true}
          caption="Enter the name of the item"
        />
        <AppFormDataTable
          propertyName="data"
          caption="Data Table"
          selectableRows
          columns={[
            {
              propertyName: "-",
              headerStyle: { width: "100px" },
              renderCell: ({ removeObject }) => (
                <Trash2 size={16} onClick={removeObject} />
              ),
            },
            { header: "ID", propertyName: "id" },
            {
              header: () => {
                return <div>Data</div>;
              },
              headerStyle: { width: "200px" },
              propertyName: "name",
              renderCell: (row) => {
                return (
                  // <AppFormFieldWrapper
                  //   propertyName={row.propertyName}
                  //   disableCaptionSpan={true}
                  //   formControlRender={(field) => {
                  //     return <>{field.value}</>;
                  //   }}
                  // />
                  <AppFormFieldInput
                    propertyName={row.propertyName}
                    isRequiredField={true}
                    disableCaptionSpan={true}
                  />
                );
              },
            },
            { header: "Description", propertyName: "description" },
          ]}
        />
        <AppFormSubmit>Submit</AppFormSubmit>
      </AppForm>
      <ResourceSchedulerProvider
        interval={60}
        resources={[
          {
            id: "resource1",
            name: "Resource 1",
            timings: [
              {
                startTime: { hour: 8, minute: 0 },
                endTime: { hour: 9, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 9, minute: 0 },
                endTime: { hour: 10, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 10, minute: 0 },
                endTime: { hour: 11, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 11, minute: 0 },
                endTime: { hour: 12, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 12, minute: 0 },
                endTime: { hour: 13, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 13, minute: 0 },
                endTime: { hour: 14, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 14, minute: 0 },
                endTime: { hour: 15, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 16, minute: 0 },
                endTime: { hour: 17, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 17, minute: 0 },
                endTime: { hour: 18, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 18, minute: 0 },
                endTime: { hour: 19, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 19, minute: 0 },
                endTime: { hour: 20, minute: 0 },
                status: "available",
              },
            ],
          },
          {
            id: "resource2",
            name: "Resource 2",
            timings: [
              {
                startTime: { hour: 9, minute: 0 },
                endTime: { hour: 10, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 10, minute: 0 },
                endTime: { hour: 11, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 11, minute: 0 },
                endTime: { hour: 12, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 12, minute: 0 },
                endTime: { hour: 13, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 13, minute: 0 },
                endTime: { hour: 14, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 14, minute: 0 },
                endTime: { hour: 15, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 16, minute: 0 },
                endTime: { hour: 17, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 17, minute: 0 },
                endTime: { hour: 18, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 18, minute: 0 },
                endTime: { hour: 19, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 19, minute: 0 },
                endTime: { hour: 20, minute: 0 },
                status: "available",
              },
            ],
          },
          {
            id: "resource3",
            name: "Resource 3",
            timings: [
              // {
              //   startTime: new Date("2025-09-07T08:00:00"),
              //   endTime: new Date("2025-09-07T10:00:00"),
              //   status: "available",
              // },
              {
                startTime: { hour: 8, minute: 0 },
                endTime: { hour: 10, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 10, minute: 0 },
                endTime: { hour: 12, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 12, minute: 0 },
                endTime: { hour: 14, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 14, minute: 0 },
                endTime: { hour: 16, minute: 0 },
                status: "unavailable",
              },
              {
                startTime: { hour: 16, minute: 0 },
                endTime: { hour: 18, minute: 0 },
                status: "available",
              },
              {
                startTime: { hour: 18, minute: 0 },
                endTime: { hour: 18, minute: 0 },
                status: "available",
              },
            ],
          },
          {
            id: "resource4",
            name: "Resource 4",
            timings: Array.from({ length: 24 }, (_, i) => ({
              startTime: { hour: i, minute: 0 },
              endTime: { hour: i + 1, minute: 0 },
              status: i % 2 === 0 ? "available" : "unavailable",
            }))
          }
        ]}
      >
        <ResourceScheduler />
      </ResourceSchedulerProvider>
    </div>
  );
}
