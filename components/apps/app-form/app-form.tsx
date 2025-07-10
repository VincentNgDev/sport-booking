"use client";

import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { cn } from "@/lib/utils";
import { Eye, EyeOff } from "lucide-react";
import React from "react";
import { ControllerRenderProps, useFieldArray, useForm } from "react-hook-form";

type FieldSpanValue = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10;

type AppFormContext = {
  form: ReturnType<typeof useForm<Record<string, any>>>;
  onSubmit: (data: any) => void;
  isPending: boolean;
  captionSpan: FieldSpanValue;
};

const appFormContext = React.createContext<AppFormContext | undefined>(
  undefined
);

export type AuthState = {
  success: boolean;
  message: string;
};

export function useAppForm() {
  const context = React.useContext(appFormContext);
  if (!context) {
    throw new Error("useAppForm must be used within an AppFormProvider");
  }
  return context;
}

export type AppFormProps = {
  initialValue?: Record<string, any> | undefined;
  children?: React.ReactNode;
  onformSubmit?: (data: any) => Promise<AuthState> | AuthState;
  captionSpan?: FieldSpanValue;
};

function AppFormProvider({
  initialValue,
  children,
  onformSubmit,
  captionSpan = 10, // Default to full width
}: AppFormProps) {
  const form = useForm({
    defaultValues: initialValue,
    mode: "onSubmit",
  });

  const [isPending, startTransition] = React.useTransition();

  const onSubmit = React.useCallback(
    async (data: any) => {
      startTransition(async () => {
        const result = await onformSubmit?.(data);
        if (result && !result.success) {
          form.setError("root", {
            type: "custom",
            message: result.message,
          });
        }
      });
    },
    [onformSubmit]
  );

  const contextValue = React.useMemo(
    () => ({
      form,
      onSubmit,
      isPending,
      captionSpan,
    }),
    [form, onSubmit, isPending, captionSpan]
  );

  return (
    <appFormContext.Provider value={contextValue}>
      {children}
    </appFormContext.Provider>
  );
}

function AppFormCore({
  children,
  className,
}: {
  children?: React.ReactNode;
  className?: string;
}) {
  const { form, onSubmit } = useAppForm();
  return (
    <Form {...form}>
      <form
        className={cn("w-full", "space-y-4", className)}
        onSubmit={form.handleSubmit(onSubmit)}
      >
        {children}

        {/* Error Message */}
        {form.formState.errors.root?.message && (
          <p className="w-full text-destructive text-sm">
            {form.formState.errors.root?.message}
          </p>
        )}
      </form>
    </Form>
  );
}

export function AppForm({
  initialValue,
  children,
  onformSubmit,
  className,
  captionSpan,
}: AppFormProps & { className?: string }) {
  return (
    <AppFormProvider
      initialValue={initialValue}
      onformSubmit={onformSubmit}
      captionSpan={captionSpan}
    >
      <AppFormCore className={className}>{children}</AppFormCore>
    </AppFormProvider>
  );
}

export function AppFormSubmit({
  children,
  Loading,
  ...props
}: Omit<React.ComponentProps<"button">, "type" | "disabled"> & {
  Loading?: React.FC | React.ElementType;
}) {
  const { isPending } = useAppForm();

  return (
    <Button type="submit" {...props} disabled={isPending}>
      {!isPending && children}
      {isPending &&
        (Loading === undefined ? <span>{"Loading..."}</span> : <Loading />)}
    </Button>
  );
}

export interface AppFormFieldBaseProps {
  propertyName: string;
  isRequiredField?: boolean;
  customValidation?: (value: any) => Promise<boolean | string>;
  caption?: string;
  disableCaptionSpan?: boolean;
  hint?: string;
  fieldSpan?: "half" | "full";
}

function processCaptionSpanClass(
  captionSpan: FieldSpanValue,
  isReverse: boolean = false
) {
  let span = captionSpan;
  if (isReverse) {
    span = 10 - captionSpan;
  }

  switch (span) {
    case 1:
      return "md:col-span-1";
    case 2:
      return "md:col-span-2";
    case 3:
      return "md:col-span-3";
    case 4:
      return "md:col-span-4";
    case 5:
      return "md:col-span-5";
    case 6:
      return "md:col-span-6";
    case 7:
      return "md:col-span-7";
    case 8:
      return "md:col-span-8";
    case 9:
      return "md:col-span-9";
    case 10:
      return "md:col-span-10";
  }
}

export function AppFormFieldWrapper({
  propertyName,
  isRequiredField,
  customValidation,
  caption,
  disableCaptionSpan = false,
  hint,
  formControlRender,
  fieldSpan = "full",
}: AppFormFieldBaseProps & {
  formControlRender: (field: ControllerRenderProps) => React.ReactElement;
}) {
  const { form, captionSpan } = useAppForm();

  return (
    <div>
      <FormField
        control={form.control}
        name={propertyName}
        render={({ field }) => {
          return (
            <FormItem>
              <div
                className={cn(
                  "space-y-2",
                  captionSpan === 10 ||
                    captionSpan === undefined ||
                    disableCaptionSpan
                    ? ""
                    : "md:grid md:grid-cols-10"
                )}
              >
                {caption && (
                  <div
                    className={cn(
                      `md:col-span-${captionSpan}`,
                      processCaptionSpanClass(captionSpan ?? 10),
                      "flex flex-row"
                    )}
                  >
                    <FormLabel className="md:flex-1">{caption}</FormLabel>
                    {!(
                      captionSpan === 10 ||
                      captionSpan === undefined ||
                      disableCaptionSpan
                    ) && <FormLabel className="px-2">{":"}</FormLabel>}
                  </div>
                )}
                <div
                  className={cn(
                    "space-y-2",
                    caption === undefined
                      ? "col-span-full"
                      : processCaptionSpanClass(captionSpan ?? 10, true)
                  )}
                >
                  <div
                    className={cn(
                      fieldSpan === "half" ? "md:w-1/2" : "md:w-full"
                    )}
                  >
                    {formControlRender && formControlRender(field)}
                  </div>

                  {hint && <FormDescription>{hint}</FormDescription>}
                  <FormMessage />
                </div>
              </div>
            </FormItem>
          );
        }}
        rules={{
          required: {
            value: isRequiredField ?? false,
            message: `This ${caption ?? propertyName} field is required`,
          },
          onBlur(event) {
            if (customValidation) {
              customValidation(event.target.value).then((result) => {
                if (result !== true) {
                  form.setError(propertyName, {
                    type: "custom",
                    message: result.toString(),
                  });
                }
              });
            }
          },
        }}
      />
    </div>
  );
}

export function AppFormFieldInput({
  onInputChange,
  type,
  ...props
}: AppFormFieldBaseProps & {
  onInputChange?: (value: any) => void;
  type?: React.HTMLInputTypeAttribute | undefined;
}) {
  const [showPassword, setShowPassword] = React.useState(false);
  const [inputType, setInputType] = React.useState(type);

  React.useEffect(() => {
    if (type && type === "password") {
      setInputType(showPassword ? "text" : "password");
    } else {
      setInputType(type);
    }
  }, [type, showPassword]);

  return (
    <AppFormFieldWrapper
      {...props}
      formControlRender={({ onChange, value, ...field }) => {
        return (
          <div className={cn("relative")}>
            <FormControl>
              <Input
                className="w-full"
                value={value ?? ""}
                type={inputType}
                onChange={(e) => {
                  onChange(e);
                  if (onInputChange) {
                    // Get the value from the input field
                    onInputChange(e.currentTarget.value);
                  }
                }}
                {...field}
              />
            </FormControl>

            {type && type === "password" && (
              <Button
                type="button"
                variant="ghost"
                size="icon"
                className="absolute right-0 top-0 h-full px-3 py-2 hover:bg-transparent"
                onClick={() => setShowPassword(!showPassword)}
              >
                {showPassword ? (
                  <EyeOff className="h-4 w-4 text-muted-foreground" />
                ) : (
                  <Eye className="h-4 w-4 text-muted-foreground" />
                )}
                <span className="sr-only">
                  {showPassword ? "Hide password" : "Show password"}
                </span>
              </Button>
            )}
          </div>
        );
      }}
    />
  );
}

type AppFormDataTableContext = {
  propertyName: string;
  removeObjectAt: (index: number) => void;
  selectObject: (index: number | number[]) => void;
};

const appFormDataTableContext = React.createContext<
  AppFormDataTableContext | undefined
>(undefined);

export interface AppFormDataTableRenderCellProps {
  propertyName: string;
  index: number;
  removeObject: () => void;
}

export interface AppFormDataTableColumnDef {
  header?: string | (() => React.ReactNode);
  propertyName: string;
  headerStyle?: React.CSSProperties;
  renderCell?: (row: AppFormDataTableRenderCellProps) => React.ReactNode;
}

export function AppFormDataTable({
  propertyName,
  caption,
  columns = [],
  selectableRows = false,
}: Pick<AppFormFieldBaseProps, "propertyName" | "caption"> & {
  columns?: AppFormDataTableColumnDef[];
  selectableRows?: boolean;
}) {
  const { form } = useAppForm();
  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: propertyName,
  });

  const [selectedObjects, setSelectedObjects] = React.useState<number[]>([]);

  const removeObjectAt = React.useCallback(
    (index: number) => {
      remove(index);
    },
    [remove]
  );

  const addObject = React.useCallback(
    (obj: any) => {
      append(obj);
    },
    [append]
  );

  const selectObject = React.useCallback((index: number | number[]) => {
    if (Array.isArray(index)) {
      setSelectedObjects(index);
    } else {
      setSelectedObjects([index]);
    }
  }, [setSelectedObjects]);

  const context = React.useMemo(
    () => ({ propertyName, removeObjectAt, selectObject }),
    [propertyName, removeObjectAt, selectObject]
  );

  return (
    <AppFormFieldWrapper
      propertyName={propertyName}
      fieldSpan="full"
      disableCaptionSpan={true}
      caption={caption}
      formControlRender={(_) => {
        return (
          <appFormDataTableContext.Provider value={context}>
            <div>
              <Table>
                <TableHeader>
                  <TableRow>
                    {selectableRows && (
                      <TableHead className="w-[50px]">
                        <Checkbox />
                      </TableHead>
                    )}
                    {columns.map(({ header, headerStyle }, index) => (
                      <TableHead key={`${header}-${index}`} style={headerStyle}>
                        {typeof header === "string"
                          ? header
                          : header && header()}
                      </TableHead>
                    ))}
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {fields.length > 0 ? (
                    fields.map((field, fieldIndex) => {
                      return (
                        <TableRow key={field.id}>
                          {selectableRows && (
                            <TableCell>
                              <Checkbox />
                            </TableCell>
                          )}
                          {columns.map((column) => {
                            return (
                              <AppFormDataTableCell
                                key={`${column.propertyName}-${fieldIndex}`}
                                propertyName={column.propertyName}
                                index={fieldIndex}
                                renderCell={column.renderCell}
                              />
                            );
                          })}
                        </TableRow>
                      );
                    })
                  ) : (
                    <TableRow>
                      <TableCell
                        colSpan={columns.length + (selectableRows ? 1 : 0)}
                        className="text-center"
                      >
                        No Data
                      </TableCell>
                    </TableRow>
                  )}
                </TableBody>
              </Table>
            </div>
          </appFormDataTableContext.Provider>
        );
      }}
    />
  );
}

function AppFormDataTableCell({
  propertyName,
  index,
  renderCell,
}: Pick<AppFormFieldBaseProps, "propertyName"> & {
  index: number;
  renderCell?: (row: AppFormDataTableRenderCellProps) => React.ReactNode;
}) {
  const context = React.useContext(appFormDataTableContext);
  if (!context) {
    throw new Error(
      "AppFormDataTableCell must be used within AppFormDataTable"
    );
  }

  const tablePropertyName = context.propertyName;
  const thisPropertyName = `${tablePropertyName}.${index}.${propertyName}`;

  const removeObject = React.useCallback(() => {
    if (context.removeObjectAt) {
      context.removeObjectAt(index);
    }
  }, [context, index]);

  return (
    <TableCell>
      {renderCell ? (
        renderCell({ propertyName: thisPropertyName, index, removeObject })
      ) : (
        <AppFormFieldWrapper
          propertyName={thisPropertyName}
          formControlRender={(field) => {
            return <>{field.value}</>;
          }}
        />
      )}
    </TableCell>
  );
}
